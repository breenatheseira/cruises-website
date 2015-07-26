<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ddac.Azure" generation="1" functional="0" release="0" Id="ee9cc1d3-21f9-48ee-b2b4-fd76d0157f25" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ddac.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="ddac:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ddac.Azure/ddac.AzureGroup/LB:ddac:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="ddac:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ddac.Azure/ddac.AzureGroup/Mapddac:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ddacInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ddac.Azure/ddac.AzureGroup/MapddacInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:ddac:Endpoint1">
          <toPorts>
            <inPortMoniker name="/ddac.Azure/ddac.AzureGroup/ddac/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="Mapddac:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ddac.Azure/ddac.AzureGroup/ddac/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapddacInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ddac.Azure/ddac.AzureGroup/ddacInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ddac" generation="1" functional="0" release="0" software="D:\Projects\v2\ddac\ddac\ddac.Azure\csx\Debug\roles\ddac" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ddac&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;ddac&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ddac.Azure/ddac.AzureGroup/ddacInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ddac.Azure/ddac.AzureGroup/ddacUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ddac.Azure/ddac.AzureGroup/ddacFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="ddacUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="ddacFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="ddacInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="cca9367a-63de-46eb-a046-4a1209905a8d" ref="Microsoft.RedDog.Contract\ServiceContract\ddac.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="d18beab7-7a82-4091-93cb-390f2b92cf83" ref="Microsoft.RedDog.Contract\Interface\ddac:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ddac.Azure/ddac.AzureGroup/ddac:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>