#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CityPowerAndLight.Model
{
	
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011/Contracts")]
	[Microsoft.Xrm.Sdk.Client.RequestProxyAttribute("DeleteMultiple")]
	public partial class DeleteMultipleRequest : Microsoft.Xrm.Sdk.OrganizationRequest
	{
		
		public Microsoft.Xrm.Sdk.EntityReferenceCollection Targets
		{
			get
			{
				if (this.Parameters.Contains("Targets"))
				{
					return ((Microsoft.Xrm.Sdk.EntityReferenceCollection)(this.Parameters["Targets"]));
				}
				else
				{
					return default(Microsoft.Xrm.Sdk.EntityReferenceCollection);
				}
			}
			set
			{
				this.Parameters["Targets"] = value;
			}
		}
		
		public DeleteMultipleRequest()
		{
			this.RequestName = "DeleteMultiple";
			this.Targets = default(Microsoft.Xrm.Sdk.EntityReferenceCollection);
		}
	}
	
	[System.Runtime.Serialization.DataContractAttribute(Namespace="http://schemas.microsoft.com/xrm/2011/Contracts")]
	[Microsoft.Xrm.Sdk.Client.ResponseProxyAttribute("DeleteMultiple")]
	public partial class DeleteMultipleResponse : Microsoft.Xrm.Sdk.OrganizationResponse
	{
		
		public DeleteMultipleResponse()
		{
		}
	}
}
#pragma warning restore CS1591
