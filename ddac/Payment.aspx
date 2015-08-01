<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ddac.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="main_bg">
    <div class="wrap">
	    <div class="main">
		    <div class="res_online">
                <p style="margin-bottom:20px">
                    <asp:Label ID="notification" runat="server" />
                </p>
                <h4><asp:Label ID="HeadingLabel" runat="server"/></h4>
            <table class="table">
                <tr>
                    <td class="auto-style2">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='Image' Width="100%" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td class="auto-style4" style="border-top: 0px">Passenger ID : </td>
                                <td class="auto-style3" style="border-top: 0px"><asp:Label ID="PassengerIDLabel" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Name : </td>
                                <td class="auto-style3"><asp:Label ID="NameLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Itinerary ID : </td>
                                <td class="auto-style3"><asp:Label ID="ItineraryIDLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Region : </td>
                                <td class="auto-style3"><asp:Label ID="RegionLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Source : </td>
                                <td class="auto-style3"><asp:Label ID="SourceLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="auto-style4">Price : </td>
                                <td class="auto-style3">$ <asp:Label ID="PriceLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Ship Name : </td>
                               <td class="auto-style3"><asp:Label ID="ShipNameLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Cruise Operator : </td>
                               <td class="auto-style3"><asp:Label ID="OperatorLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Booking Date : </td>
                               <td class="auto-style3"><asp:Label ID="BDateLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Journey Date : </td>
                               <td class="auto-style3"><asp:Label ID="JDateLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Cabin Type : </td>
                               <td class="auto-style3"><asp:Label ID="CabinTypeLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Cabin Price : </td>
                               <td class="auto-style3">$ <asp:Label ID="CabinPriceLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Cabin Capacity : </td>
                               <td class="auto-style3"><asp:Label ID="CabinCapacityLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Total Price : </td>
                               <td class="auto-style3">$ <asp:Label ID="TotalPriceLabel" runat="server" ></asp:Label></td>
                            </tr>
                            <tr>
                               <td class="auto-style4">Payment Status : </td>
                               <td class="auto-style3"><asp:Label ID="PaymentStatusLabel" runat="server" Font-Bold="true"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>               
            </table>
            <div class="res_btn">
                <asp:Button ID="PayButton" runat="server" Text="Pay with Paypal" OnClick="PayButton_Click" CssClass="btn btn-primary btn-lg "/>
            </div>
        </div>
    </div>
</div>
</asp:Content>
