<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyBooking.aspx.cs" Inherits="ddac.MyBooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#datepicker,#datepicker1").datepicker();
        });
	</script>
    <style type="text/css">
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
            <p>
                <asp:Label ID="notification" runat="server"></asp:Label> 
            </p>
            <asp:DataList ID="MyBookingList" runat="server"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" CellSpacing="2" Font-Bold="True"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" Width="100%">
            <ItemStyle/>
            <ItemTemplate>
                <h4>BookingID #<asp:Label ID="BookingIDLabel" Text='<%# Eval("BookingID") %>' runat="server"></asp:Label></h4>
                <table> 
                    <tr>
                        <td class="auto-style4">ItineraryID: </td>
                        <td>
                            <asp:Label ID="ItineraryIDLabel" Text='<%# Eval("ItineraryID") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Journey Date: </td>
                        <td>
                            <asp:Label ID="JourneyDateLabel" Text='<%# Eval("JourneyDate") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Cabin Name: </td>
                        <td>
                            <asp:Label ID="CabinNameLabel" Text='<%# Eval("CabinName") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Total Price: </td>
                        <td>
                            <asp:Label ID="TotalPrice" Text='<%# Eval("TotalPrice") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Booking Status: </td>
                        <td>
                            <asp:Label ID="BookingStatusLabel" Text='<%# Eval("BookingStatus") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">Capacity: </td>
                        <td>
                            <asp:Label ID="PaymentStatusLabel" Text='<%# Eval("PaymentStatus") %>' runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <SelectedItemStyle Font-Bold="True" ForeColor="White" />
        </asp:DataList>
        </div>			
	</div>
</div>
</div>		
<!--start main -->
</asp:Content>