using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Arclight.TableExtractor
{
    public class PasswordExtractor
    {
        private readonly Dictionary<string, string> archivePasswords;

        /// <summary>
        /// Create a new <see cref="PasswordExtractor"/> with the supplied path to the base.dll.
        /// </summary>
        public PasswordExtractor(string path)
        {
            Span<byte> file = File.ReadAllBytes(path);

            int? offset = FindArray(file, Encoding.ASCII.GetBytes("data??.v"));
            if (offset == null)
                throw new InvalidDataException();

            int currentOffset = offset.Value;

            // base contains an array of archive file names with passwords and a second with the passwords
            // first iterate through all of the archive names
            var archives = new List<string>();
            while (true)
            {
                string str = ReadString(file, currentOffset);
                if (!str.StartsWith("data"))
                    break;

                archives.Add(str);
                currentOffset += Align(str.Length + 1, 4);
            }

            // once we know how many archives have passwords loop through and get them
            var passwords = new List<string>();
            for (var i = 0; i < archives.Count; i++)
            {
                string str = ReadString(file, currentOffset);
                passwords.Add(str);
                currentOffset += Align(str.Length + 1, 4);
            }

            // merge separate lists for archives and passwords
            archivePasswords = archives
                .Zip(passwords, (a, b) => new { a, b })
                .ToDictionary(p => p.a, p => p.b);

            Console.WriteLine($"Found {passwords.Count} passwords.");
            foreach ((string archive, string password) in archivePasswords)
                Console.WriteLine($"{archive}:{password}.");
        }

        private int? FindArray(Span<byte> data, Span<byte> match)
        {
            for (int i = 0; i < data.Length - match.Length; i++)
            {
                Span<byte> segment = data.Slice(i, match.Length);
                if (Matches(segment, match))
                    return i;
            }

            return null;
        }

        private bool Matches(Span<byte> data, Span<byte> match)
        {
            for (int i = 0; i < data.Length; i++)
                if (data[i] != match[i] && match[i] != '?')
                    return false;

            return true;
        }

        private string ReadString(Span<byte> data, int offset)
        {
            var list = new List<char>();
            while (true)
            {
                char b = (char)data[offset++];
                if (b == '\0')
                    return new string(list.ToArray());

                list.Add(b);
            }
        }

        private int Align(int value, int align)
        {
            int alignedValue = value;
            if (alignedValue % align != 0)
                alignedValue += align - alignedValue % align;

            return alignedValue;
        }

        /// <summary>
        /// Return password for supplied archive.
        /// </summary>
        public string GetPassword(string archive)
        {
            return archivePasswords.TryGetValue(archive, out string password) ? password : null;
        }
    }
}
