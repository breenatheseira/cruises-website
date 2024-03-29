﻿<%@ Page enableEventValidation="False" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Itinerary.aspx.cs" Inherits="ddac.Itinerary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
		$(document).ready(function() {
		    $("#fDate,#tDate").datepicker();		    
		});
	</script>
    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
        }
    </script>
    <style type="text/css">
        .Text_Align{
            text-align: center;
            padding-top: 10px;            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <!--start main -->
<div class="main_bg">
<div class="wrap">
	<div class="main">
<!-- start main_content -->
        <!--start main -->
	    <div class="online_reservation">
	    <div class="b_room">
		    <div class="reservation">
                <h3 style="padding:10px 0px 20px 0px;font-size:large">Enter the fields below to find itineraries:</h3>
			    <ul>
				    <li class="span1_of_1">
					    <h5>Region:</h5>
					    <!----------start section_region----------->
					    <div class="section_room">
                            <asp:DropDownList ID="RegionDropDown" runat="server" DataSourceID="ItineraryCode" DataTextField="Region" AppendDataBoundItems="true" DataValueField="Region" class="frm-field required">
                                <asp:ListItem Text="All Cruises" Value="" Selected="True"/>
                            </asp:DropDownList>                            
                            <asp:SqlDataSource ID="ItineraryCode" runat="server" ConnectionString="<%$ ConnectionStrings:DDACConnection %>" SelectCommand="SELECT Distinct Region FROM [Itinerary] "></asp:SqlDataSource>
					    </div>	
				    </li>
				    <li  class="span1_of_1 left">
					    <h5>From:</h5>
					    <div class="book_date">
						    <form>
								<input class="date" id="fDate" name="fDate" type="text" value="DD/MM/YYYY" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'DD/MM/YY';}">
						    </form>

					    </div>					
				    </li>
				    <li  class="span1_of_1 left">
					    <h5>To:</h5>
					    <div class="book_date">
							<input class="date" id="tDate" name="tDate" type="text" value="DD/MM/YYYY" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'DD/MM/YY';}">
					    </div>		
				    </li>
				    <li class="span1_of_1 left">
					    <h5>Price Range:</h5>
					    <div class="section_price">
						    <select id="price" onchange="change_price(this.value)" name="price" class="frm-field required">
							    <option value="1000000">Any Price</option>
                                <option value="1000">< $1000</option>
				                <option value="1500">< $1500</option>         
				                <option value="2000">< $2000</option>
							    <option value="3000">< $3000</option>
		        		    </select>
					    </div>					
				    </li>
                    <li style="padding-left:14px;padding-top:27px">
                        <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click" CssClass="btn btn-primary btn-lg">
                            <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                        </asp:LinkButton>
                    </li>
				    <div class="clear"></div>
			    </ul>
		    </div>
		    <div class="clear"></div>
		    </div>
        <p>
            <asp:Label ID="notification" runat="server"></asp:Label> 
        </p>
        <asp:DataList ID="ItineraryList" runat="server" BorderStyle="None" Font-Names="Verdana" Font-Size="Small" GridLines="Both" ItemStyle-CssClass="Text_Align"
            RepeatColumns="4" RepeatDirection="Horizontal" Width="100%" ItemStyle-HorizontalAlign="Center" RepeatLayout="Table">
        <ItemStyle />
        <ItemTemplate>
            <table> 
                <tr>
                    <td colspan="2" style="margin:10px"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("ItineraryDetails") %>' Width="210px" 
                        Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>
                </tr>
                <tr>
                    <td>ID: </td>
                    <td>
                        <h3><asp:Label ID="ItineraryIDLabel" Text='<%# Eval("ItineraryID") %>' runat="server"></asp:Label></h3>
                    </td>
                </tr>
                <tr>
                    <td>Region: </td>
                    <td>
                        <asp:Label ID="RegionLabel" Text='<%# Eval("Region") %>' runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Source: </td>
                    <td>
                        <asp:Label ID="SourceLabel" Text='<%# Eval("Source") %>' runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Price: </td>
                    <td>
                        $<asp:Label ID="PriceLabel" Text='<%# Eval("Price") %>' runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Ship: </td>
                    <td>
                        <asp:Label ID="ShipNameLabel" Text='<%# Eval("ShipID") %>' runat="server"></asp:Label>
                    </td>
                </tr>
				<tr>
                    <td>Journey Date: </td>
                    <td>
                        <asp:Repeater ID="JourneyDateList" runat="server" DataSource='<%# GetJourneyDate((int)(Eval("ItineraryID"))) %>'>    
                            <ItemTemplate>
                                <asp:Label ID="JourneyDateLabel" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"JD","{0:MMM dd, yyyy}<br/>") %>'/>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <h4><a href="Booking.aspx?ItineraryID=<%# Eval("ItineraryID") %>" class="btn btn-primary">Book now</a></h4>
                    </td>
                </tr>
            </table>
        </ItemTemplate>        
        <SelectedItemStyle Font-Bold="True" ForeColor="White" />
    </asp:DataList>
    </div>
	<div class="clear"></div>
	<!-- end main_content -->
    <div id="divBackground" class="modal">
    </div>
    <div id="divImage">
    <table style="height: 100%; width: 100%">
        <tr>
            <td valign="middle" align="center">
                <img id="imgLoader" alt="" src="images/loader.gif" />
                <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
            </td>
        </tr>
        <tr>
            <td align="center" valign="bottom">
                <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
            </td>
        </tr>
    </table>
    </div>
	</div>
</div>
</div>		
<!--start main -->
</asp:Content>