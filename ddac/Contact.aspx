<%@ Page enableEventValidation="False" Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ddac.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<!--start main -->
<div class="main_bg">
<div class="wrap">
	<div class="main">
		<div class="contact">				
				<div class="contact_left">
      			<div class="company_address">
                      <h3>Contact Details</h3>
				     	<p>Carnival PLC (Singapore Branch):</p>
						<p>Marina Bay Financial Centre,</p>
						<p>10 Marina Boulevard Tower 2, #14-02, Lorem,</p>
						<p>Singapore 018983</p>
				   		<p>Phone:(65) 6922 6788</p>				   		
				 	 	<p>Email: <a href="mailto:carnival.corporation@mail.com">support@carnival-sg.com</a></p>
				   		<p>Follow on: <a href="https://www.facebook.com/PrincessCruises?fref=ts">Facebook</a>, <a href="https://twitter.com/PrincessCruises">Twitter</a></p>
				   </div>
				</div>				
				<div class="contact_right">
				  <div class="contact-form">
				  	<h3>Contact Us</h3>					    
					    <div>
						    <span><label>NAME</label></span>
						    <span><asp:TextBox ID="name" runat="server"/></span>
						</div>
						<div>
						    <span><label>E-MAIL</label></span>
						    <span><asp:TextBox ID="email" runat="server" TextMode="Email" CssClass="contact-form span"/></span>
						</div>
						<div>
						    <span><label>MOBILE</label></span>
						    <span><asp:TextBox ID="mobile" runat="server"/></span>
						</div>
						<div>
						    <span><label>SUBJECT</label></span>
						    <span><asp:TextBox ID="subject" runat="server"/></span>
						</div>
                        <div>
						    <span><label>MESSAGE</label></span>
						    <span><asp:TextBox ID="message" TextMode="MultiLine" runat="server"/></span>
						</div>
						<div>
						   	<span><asp:Button ID="Submit" OnClick="Submit_Click" runat="server" Text="Send Mail"/></span>
						</div>
				    </div>
  				</div>		
  				<div class="clear"></div>
            <asp:RegularExpressionValidator ID="mobileRegulator" runat="server" ErrorMessage="Mobile is not in the correct format." ControlToValidate="mobile" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="nameRequired" runat="server" ErrorMessage="Name is Required." Display="Dynamic" ControlToValidate="name" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="emailRequired" runat="server" ErrorMessage="Email is Required." Display="Dynamic" ControlToValidate="email" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="subjectRequired" runat="server" ErrorMessage="Subject is Required." Display="Dynamic" ControlToValidate="subject" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="messageRequired" runat="server" ErrorMessage="Message is Required." Display="Dynamic" ControlToValidate="message" ForeColor="Red"></asp:RequiredFieldValidator>
		  </div>
	</div>
</div>
</div>		
<!--start main -->
</asp:Content>

