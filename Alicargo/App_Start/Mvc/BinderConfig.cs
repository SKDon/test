using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Alicargo.Mvc
{
	internal static class BinderConfig
	{
		public static void RegisterBinders(ModelBinderDictionary binders)
		{
			var types = Assembly.GetCallingAssembly().GetTypes();

			foreach (var binderType in types.Where(x => x.BaseType == typeof(DefaultModelBinder)))
			{
				var binder = (IModelBinder)Activator.CreateInstance(binderType);
				var modelName = binderType.Name.Substring(0, binderType.Name.Length - "Binder".Length);

				var type = types.First(y => y.Name == modelName);

				binders.Add(type, binder);
			}
		}
	}
}