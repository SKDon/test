using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Alicargo.Core.Localization
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Field)]
	public sealed class DisplayNameLocalizedAttribute : DisplayNameAttribute
	{
		private readonly Type _resourceType;
		private readonly string _resourceKey;
		private static readonly ConcurrentDictionary<Type, ResourceManager> ResourceManagers = new ConcurrentDictionary<Type, ResourceManager>();

		public DisplayNameLocalizedAttribute(Type resourceType, string resourceKey)
		{
			_resourceType = resourceType;
			_resourceKey = resourceKey;
		}

		public override string DisplayName
		{
			get
			{
				return LookupResource(_resourceType, _resourceKey) ?? DisplayNameValue;
			}
		}

		internal static string LookupResource(Type resourceType, string resourceKey)
		{
			var manager = ResourceManagers.GetOrAdd(resourceType,
				type => resourceType.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
					.Where(info => info.PropertyType == typeof(ResourceManager))
					.Select(x => (ResourceManager)x.GetValue(null, null))
					.FirstOrDefault());

			return manager != null ? (manager.GetString(resourceKey) ?? resourceKey) : resourceKey;
		}
	}
}
