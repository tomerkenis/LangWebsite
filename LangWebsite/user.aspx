<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/user.css" type="text/css" rel="Stylesheet">
</head>
<body>
    <div class="nav">
        <div class="hamburger">
            <div class="line"></div>
            <div class="line"></div>
            <div class="line"></div>
        </div>
        <ul class="nav-links">
            <li class="nav-link"><a href="index.aspx" class="nav-label">About</a></li>
            <li class="nav-link"><a href="gallery.aspx" class="nav-label">Gallery</a></li>
            <li class="nav-link"><a href="board.aspx" class="nav-label">Board</a></li>
             <% if (Session["user"] != null && LocalVars.ADMINS.Contains(Session["user"]) ) 
                     Response.Write("<li class=\"nav-link\"><a href=\"admin.aspx\" class=\"nav-label\">Admin</a></li>");%>
            <li class="nav-link"><a href="user.aspx" class="nav-label">User</a></li>
            <li class="nav-link"><span class="nav-label sign-in">Sign In</span></li>
        </ul>
    </div>

    

    <div class="overlay"></div>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <%--<script type="text/javascript" src="js/login.js"></script>--%>

    <form method="post" class="sign-in-overlay" 
       <%-- onsubmit="return validateLoginAll()" --%>
        >
            <div class="email">
                <%--<label for="email" class="sign-in-label">Email:</label>--%>
                <input type="text" id="email" name="email" class="sign-in-field" placeholder="Email" 
                    <%if (Session["user"] != null) Response.Write("disabled=\"disabled\""); %>/>
            </div>
            <div class="password">
                <%--<label for="password" class="sign-in-label">Password:</label>--%>
                <input type="password" id="password" name="password" class="sign-in-field" placeholder="Password" 
                    <%if (Session["user"] != null) Response.Write("disabled=\"disabled\""); %>>
            </div>
            <%--<div class="remember-me">
                <input type="checkbox" id="remember-me" name="remember-me" value="remember">
                <label for="remember-me">Remember me</label>
            </div>--%>
            <%=msg %>
            <input type="submit" name="login" value="Sign in / Sign out" class="submit">
            <div class="long-line"></div>
            <a href="register.aspx" class="sign-up" style="margin-bottom: 15px;"><span>Sign up</span></a>
            <%=state %>
    </form>

    <script type="text/javascript" src="js/hamburger.js"></script>
    
    

    <script type="text/javascript" src="js/register.js"></script>

    <div class="content">
        <%if (Session["user"] == null)
        {
            Response.Write("<font color='red'>You need to sign in to view this page!</font>");
            Response.End();
        }
        %>
            <form method="post" name="myform" id="myform" class="form-qs" onsubmit="return validateAll()">
                <div class="form-part">
                <div class="form-text">
                    <label for="email-new">Email:</label>
                    <input value="<%=emailCurrent %>" type="text" name="email-new" id="email-new" class="sign-up-input" disabled="disabled"/>
                </div>
                <div class="form-text">
                    <label for="username-new">Username:</label>
                    <input value="<%=usernameCurrent %>" type="text" name="username-new" id="username-new" class="sign-up-input" disabled="disabled"/>
                </div>
                <div class="form-text">
                    <label for="password-new">Password:</label>
                    <input type="password" name="password-new" id="password-new" class="sign-up-input" />
                    <p class="error-password error">
                        Your password must contain:
                        <br/> - At least 1 uppercase character
                        <br/> - At least 8 letters
                        <br/> - At least 1 number
                        <br/> - At least 1 special letter
                    </p>
                </div>
                <div class="form-age">
                    <label for="gender" style="margin: 10px 0;">Gender:</label>
                    <div class="radio-option"><input type="radio" name="gender" id="male" value="male"  <%if (genderCurrent == "male") Response.Write("checked='checked'"); %>/> Male </div>
                    <div class="radio-option"><input type="radio" name="gender" id="female" value="female"  <%if (genderCurrent == "female") Response.Write("checked='checked'"); %>/> Female </div>
                    <div class="radio-option"><input type="radio" name="gender" id="printer" value="printer"  <%if (genderCurrent == "printer") Response.Write("checked='checked'"); %>/> HP LaserJet Pro P1102 Printer </div>
                    <div class="radio-option"><input type="radio" name="gender" id="other" value="other"  <%if (genderCurrent == "other") Response.Write("checked='checked'"); %>/> Other </div>
                </div>
                <div class="form-select" style="margin: 10px 0;">
                    <label for="age" >Age:</label>
                    <select name="age" id="age">
                        <option <%if (ageCurrent == "0") Response.Write("selected"); %> value="0">0-14</option>
                        <option <%if (ageCurrent == "14") Response.Write("selected"); %> value="14">14-18</option>
                        <option <%if (ageCurrent == "18") Response.Write("selected"); %> value="18">18-24</option>
                        <option <%if (ageCurrent == "24") Response.Write("selected"); %> value="24">24-40</option>
                        <option <%if (ageCurrent == "40") Response.Write("selected"); %> value="40">40-60</option>
                        <option <%if (ageCurrent == "60") Response.Write("selected"); %> value="60">60+</option>
                    </select>
                </div>
                </div>

                <div class="form-part" style="margin-left: 10vw;">
                    <div class="form-comments">
                        <label for="languages" style="margin: 10px 0;">Tell us more about yourself!</label>
                        <textarea rows="8" cols="40" name="comments" id="comments" ><%=commentsCurrent %></textarea>
                    </div>
                    <input type="submit" name="update" value="Change Credentials" class="button"/>
                    <%--<input type="reset" name="clear" value="Clear" class="button"/>--%>
                </div>
            </form>
        <%=msgRegister %>
    </div>

</body>
</html>
