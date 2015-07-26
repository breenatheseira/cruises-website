<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ddac.Azure1" generation="1" functional="0" release="0" Id="a1f3535f-25a7-4308-b2ba-5ed2990ef902" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ddac.Azure1Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="ddac:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ddac.Azure1/ddac.Azure1Group/LB:ddac:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="ddac:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ddac.Azure1/ddac.Azure1Group/Mapddac:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ddacInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ddac.Azure1/ddac.Azure1Group/MapddacInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:ddac:Endpoint1">
          <toPorts>
            <inPortMoniker name="/ddac.Azure1/ddac.Azure1Group/ddac/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="Mapddac:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ddac.Azure1/ddac.Azure1Group/ddac/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapddacInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ddac.Azure1/ddac.Azure1Group/ddacInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="ddac" generation="1" functional="0" release="0" software="D:\Projects\v2\ddac\ddac\ddac.Azure1\csx\Debug\roles\ddac" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
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
            <sCSPolicyIDMoniker name="/ddac.Azure1/ddac.Azure1Group/ddacInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ddac.Azure1/ddac.Azure1Group/ddacUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ddac.Azure1/ddac.Azure1Group/ddacFaultDomains" />
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
    <implementation Id="c2bfd324-da21-466b-b95e-822f23eae0ad" ref="Microsoft.RedDog.Contract\ServiceContract\ddac.Azure1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="c4e92b49-66b2-4043-851b-1b63bfc70fd1" ref="Microsoft.RedDog.Contract\Interface\ddac:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ddac.Azure1/ddac.Azure1Group/ddac:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>