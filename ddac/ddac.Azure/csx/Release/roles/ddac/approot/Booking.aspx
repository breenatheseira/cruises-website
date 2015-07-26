<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="ddac.Booking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#datepicker,#datepicker1").datepicker();
        });
	</script>
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
            <asp:DataList ID="CabinList" runat="server"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="2" Font-Bold="True"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="4" RepeatDirection="Horizontal"
            Width="100%">
            <ItemStyle/>
            <ItemTemplate>
                <h3><asp:Label ID="CabinNameLabel" Text='<%# Eval("CabinName") %>' runat="server"></asp:Label></h3>
                <table> 
                    <tr>
                        <td>Cabin Price: </td>
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
                </table>
            </ItemTemplate>
            <SelectedItemStyle Font-Bold="True" ForeColor="White" />
        </asp:DataList>
        </div>			
			<div class="span_of_2">
				<div class="span2_of_1">
					<h4>check-in:</h4>
					<div class="book_date btm">
						<form>
							<input class="date" id="datepicker" type="text" value="DD/MM/YY" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'DD/MM/YY';}">
						</form>
					</div>	
					<div class="sel_room">
						<h4>number of rooms</h4>
						<select id="country" onchange="change_country(this.value)" class="frm-field required">
							<option value="null">Select a type of Room</option>
				            <option value="null">Suite room</option>         
				            <option value="AX">Single room</option>
							<option value="AX">Double room</option>
		        		</select>
					</div>	
					<div class="sel_room left">
						<h4>adults per room:</h4>
						<select id="country" onchange="change_country(this.value)" class="frm-field required">
							<option value="null">1</option>
				            <option value="null">2</option>         
				            <option value="AX">3</option>
							<option value="AX">4</option>
		        		</select>
					</div>	
				</div>
				<div class="span2_of_1">
					<h4>check-out:</h4>
					<div class="book_date btm">
						<form>
							<input class="date" id="datepicker1" type="text" value="DD/MM/YY" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'DD/MM/YY';}">
						</form>
					</div>	
					<div class="sel_room">
						<h4>childern 0-5:</h4>
						<select id="country" onchange="change_country(this.value)" class="frm-field required">
							<option value="null">0</option>
							<option value="null">1</option>
				            <option value="null">2</option>         
				            <option value="AX">3</option>
							<option value="AX">4</option>
		        		</select>
					</div>	
					<div class="sel_room left">
						<h4>childern 6-12:</h4>
						<select id="country" onchange="change_country(this.value)" class="frm-field required">
							<option value="null">0</option>
							<option value="null">1</option>
				            <option value="null">2</option>         
				            <option value="AX">3</option>
							<option value="AX">4</option>
		        		</select>
					</div>	
				</div>
				<div class="clear"></div>
			</div>
			<div class="res_btn">
				<form>
					<input type="submit" value="Add to Wish List" style="width: 280px;">
                    <input type="submit" value="book now" style="width: 280px;">
				</form>
			</div>
	</div>
</div>
</div>		
<!--start main -->
</asp:Content>

