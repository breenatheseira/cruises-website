<%@ Page enableEventValidation="False" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="ddac.Booking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 174px;
        }
        .auto-style2 {
            width: 344px;
        }
        .auto-style3 {
            width: 371px;
        }
        .auto-style4 {
            width: 214px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <!--start main -->
<div class="main_bg">
<div class="wrap">
	<div class="main">
		<div class="res_online">
			<h4>ItineraryID: #<asp:Label ID="ItineraryIDLabel" runat="server" Text="Label"></asp:Label></h4>
            <table class="table">
                <tr>
                    <td class="auto-style2">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='Image' Width="100%" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="auto-style4" style="border-top: 0px">Region : </td>
                                <td class="auto-style3" style="border-top: 0px"><asp:Label ID="RegionLabel" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Source : </td>
                                <td class="auto-style3"><asp:Label ID="SourceLabel" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Price : </td>
                                <td class="auto-style3"><asp:Label ID="PriceLabel" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Ship Name : </td>
                               <td class="auto-style3"><asp:Label ID="ShipNameLabel" runat="server" Text="Label"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>               
            </table>
            <p>
                <asp:Label ID="notification" runat="server"></asp:Label> 
            </p>
			<div class="span_of_2">
				<div class="span2_of_1">
					<h4>Journey Date:</h4>
					<div class="book_date btm">
						<asp:DropDownList ID="dateDDL" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="span2_of_1">
					<h4>Cabin Selection:</h4>
					<div class="book_date btm">
                        <asp:DropDownList ID="cabinDDL" runat="server"></asp:DropDownList>
					</div>
                </div>
                <asp:DataList ID="CabinList" runat="server"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="2" Font-Bold="True"
                    Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal"
                    Width="100%">
                    <ItemStyle/>
                    <ItemTemplate>                        
                        <h3><asp:Label ID="CabinNameLabel" Text='<%# Eval("CabinName") %>' runat="server"></asp:Label></h3>
                        <table> 
                            <tr>
                                <td>Cabin Price: $</td>
                                <td>
                                    <asp:Label ID="CabinPriceLabel" Text='<%# Eval("CabinPrice") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Capacity: </td>
                                <td>
                                    <asp:Label ID="CapacityLabel" Text='<%# Eval("Capacity") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Available: </td>
                                <td>
                                    <asp:Label ID="Label1" Text='<%# Eval("Available") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SelectedItemStyle Font-Bold="True" ForeColor="White" />
                </asp:DataList>
                <div class="clear"></div>
            </div>	
        </div>
		<div class="res_btn">
			<form>
                <asp:Button ID="BookButton" runat="server" Text="Book Now" OnClick="BookButton_Click" CssClass="btn btn-primary btn-lg"/>
			</form>
		</div>
	</div>
</div>
</div>		
<!--start main -->
</asp:Content>