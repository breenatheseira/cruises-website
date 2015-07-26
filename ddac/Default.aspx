<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ddac._Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#fDate").datepicker();
            $("#tDate").datepicker();
        });
	</script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <!----start-images-slider---->
		    <div class="images-slider">
			    <!-- start slider -->
		        <div id="fwslider">
		            <div class="slider_container">
		                <div class="slide"> 
		                    <!-- Slide image -->
		                        <img src="Images/slider-bg.jpg" alt=""/>
		                    <!-- /Slide image -->
		                    <!-- Texts container -->
		                    <div class="slide_content">
		                        <div class="slide_content_wrap">
		                            <!-- Text title -->
		                            <h4 class="title" ><i class="bg"></i>Princess Cruise</h4>
		                            <h5 class="title1"><i class="bg"></i>Join our exclusive cruises</h5>
		                            <!-- /Text title -->
		                        </div>
		                    </div>
		                     <!-- /Texts container -->
		                </div>
		                <!-- /Duplicate to create more slides -->
		                <div class="slide">
		                    <img src="Images/slider-bg.jpg" alt=""/>
		                    <div class="slide_content">
		                         <div class="slide_content_wrap">
		                            <!-- Text title -->
		                            <h4 class="title"><i class="bg"></i>Cunard Line</h4>
		                            <h5 class="title1"><i class="bg"></i>Cruise over the Atlantic Ocean</h5>
		                            <!-- /Text title -->
		                        </div>
		                    </div>
		                </div>
		                <!--/slide -->
		            </div>
		            <div class="timers"> </div>
		            <div class="slidePrev"><span> </span></div>
		            <div class="slideNext"><span> </span></div>
		        </div>
		        <!--/slider -->
		    </div>
    <!--start main -->
    <div class="main_bg">
    <div class="wrap">
	    <div class="online_reservation">
	    <div class="b_room">
		    <div class="booking_room">
			    <h4>Find Your Perfect Cruise</h4>
		    </div>
		    <div class="reservation">
			    <ul>
				    <li class="span1_of_1">
					    <h5>Region:</h5>
					    <!----------start section_region----------->
					    <div class="section_room">
                            <asp:DropDownList ID="RegionDropDown" runat="server" DataSourceID="ItineraryCode" DataTextField="Region" DataValueField="Region" class="frm-field required"></asp:DropDownList>
                            <asp:SqlDataSource ID="ItineraryCode" runat="server" ConnectionString="<%$ ConnectionStrings:DDACConnection %>" SelectCommand="SELECT Distinct Region FROM [Itinerary] "></asp:SqlDataSource>
					    </div>	
				    </li>
				    <li  class="span1_of_1 left">
					    <h5>From:</h5>
					    <div class="book_date">
						    <form>
							    <input class="date" id="fDate" name="fDate" type="text" value="DD/MM/YY" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'DD/MM/YY';}">
						    </form>
					    </div>					
				    </li>
				    <li  class="span1_of_1 left">
					    <h5>To:</h5>
					    <div class="book_date">
						    <form>
							    <input class="date" id="tDate" name="tDate" type="text" value="DD/MM/YY" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'DD/MM/YY';}">
						    </form>
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
				    <div class="clear"></div>
			    </ul>
                <div class="date_btn">
                    <asp:Button ID="SearchButton" runat="server" Text="Search Itinerary" OnClick="SearchButton_Click" CssClass="btn btn-default"/>
                </div>
		    </div>
		    <div class="clear"></div>
		    </div>
	    </div>
	    <!--start grids_of_3 -->
	    <div class="grids_of_3">
		    <div class="grid1_of_3">
			    <div class="grid1_of_3_img">
				    <a href="details.html">
					    <img src="Images/pic2.jpg" alt="" />
					    <span class="next"> </span>
				    </a>
			    </div>
			    <h4><a href="#">single room<span>120$</span></a></h4>
			    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
		    </div>
		    <div class="grid1_of_3">
			    <div class="grid1_of_3_img">
				    <a href="details.html">
					    <img src="Images/pic1.jpg" alt="" />
					    <span class="next"> </span>
				    </a>
			    </div>
			    <h4><a href="#">double room<span>180$</span></a></h4>
			    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
		    </div>
		    <div class="grid1_of_3">
			    <div class="grid1_of_3_img">
				    <a href="details.html">
					    <img src="Images/pic3.jpg" alt="" />
					    <span class="next"> </span>
				    </a>
			    </div>
			    <h4><a href="#">suite room<span>210$</span></a></h4>
			    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
		    </div>
		    <div class="clear"></div>
	    </div>	
    </div>
    </div>		
    <!--start main -->
</asp:Content>