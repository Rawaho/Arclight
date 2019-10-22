using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Arclight.Shared.Command.Context;
using Arclight.Shared.Command.Convert;

namespace Arclight.Shared.Command
{
    public class CommandHandler
    {
        private Delegate handlerDelegate;
        private readonly List<ICommandParameterConverter> additionalParameterConverters = new List<ICommandParameterConverter>();

        public CommandHandler(Type type, MethodInfo method)
        {
            InitialiseDelegate(type, method);
            InitialiseParameters(method);
        }

        private void InitialiseDelegate(Type type, MethodInfo method)
        {
            // make sure the first parameter for the handler is the session
            ParameterInfo[] info = method.GetParameters();
            if (info.Length < 1 || !typeof(ICommandContext).IsAssignableFrom(info[0].ParameterType))
                throw new CommandException("");

            ParameterExpression instance = Expression.Parameter(type);

            var parameters = new List<ParameterExpression>{ instance };
            parameters.AddRange(info.Select(p => Expression.Parameter(p.ParameterType)));

            MethodCallExpression call = Expression.Call(instance, method, parameters.Skip(1));
            LambdaExpression lambda = Expression.Lambda(call, parameters);
            handlerDelegate = lambda.Compile();
        }

        private void InitialiseParameters(MethodInfo method)
        {
            foreach (Type parameterType in method.GetParameters()
                .Skip(1)
                .Select(p => p.ParameterType))
            {
                ICommandParameterConverter converter = CommandManager.Instance.GetConverter(parameterType);
                if (converter == null)
                    throw new CommandException("");

                additionalParameterConverters.Add(converter);
            }
        }

        public CommandResult Invoke(ICommandCategory category, ICommandContext context, string[] parameters, uint depth)
        {
            var additionalParameterCount = parameters.Length - depth;
            if (additionalParameterCount < additionalParameterConverters.Count)
                return CommandResult.Parameter;

            var parameterObjects = new List<object> { category, context };
            for (int i = 0; i < additionalParameterConverters.Count; i++)
            {
                if (!additionalParameterConverters[i].TryConvert(parameters[depth + i], out object result))
                    return CommandResult.Parameter;

                parameterObjects.Add(result);
            }

            handlerDelegate.DynamicInvoke(parameterObjects.ToArray());
            return CommandResult.Ok;
        }
    }
}
