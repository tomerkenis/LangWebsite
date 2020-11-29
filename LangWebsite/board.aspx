﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="board.aspx.cs" Inherits="board" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/board.css" type="text/css" rel="Stylesheet">
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

    <div class="content">
        <%=posts %>
        <% if (Session["user"] == null) Response.End();%>
    </div>

    
    <form method="post" class="to-post">
        <textarea name="send-text" rows="8" cols="40" class="textarea"></textarea>
        <input type="submit" name="send" class="send" value="Send"/>
    </form>


</body>
</html>
