<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ddac.Payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">

    <input type="hidden" name="cmd" value="_xclick" />
    <input type="hidden" name="business" value="breenatheseira-facilitator@yahoo.com" />

    <input type="hidden" name="item_name" value="test"/>
    <input type="hidden" name="amount" value="10"/> 
    <input type="submit" value="Buy!" />

</form>
</body>
</html>
