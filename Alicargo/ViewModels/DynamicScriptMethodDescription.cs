using System;

namespace Alicargo.ViewModels
{
	public sealed class DynamicScriptMethodDescription
	{
		public string Action { get; set; }
		public string Controller { get; set; }
		public string Area { get; set; }

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			if(ReferenceEquals(this, obj)) return true;
			return obj is DynamicScriptMethodDescription && Equals((DynamicScriptMethodDescription)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Action != null ? Action.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Controller != null ? Controller.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Area != null ? Area.GetHashCode() : 0);
				return hashCode;
			}
		}

		private bool Equals(DynamicScriptMethodDescription other)
		{
			return string.Equals(Action, other.Action, StringComparison.InvariantCultureIgnoreCase) &&
			       string.Equals(Controller, other.Controller, StringComparison.InvariantCultureIgnoreCase) &&
			       string.Equals(Area, other.Area, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}