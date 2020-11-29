<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gallery.aspx.cs" Inherits="gallery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/gallery.css" type="text/css" rel="Stylesheet">
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
         <% if (Session["user"] == null)
          {
              Response.Write("<font color='red'>You need to sign in to view this page!</font>");
              Response.End();
          }
          %>
       <div class="imgs">
               <div class="img-container"><img data-enlargeable alt="Slavic languages tree" src="imgs/Slavic-Tree.png" /></div>
               <div class="img-container"><img data-enlargeable alt="Indo-european languages tree part 1" src="imgs/Indo-european-part1.png" /></div>
               <div class="img-container"><img data-enlargeable alt="Indo-european languages tree part 2" src="imgs/Indo-european-part2.png" /></div>
               <div class="img-container"><img data-enlargeable alt="Language families" src="imgs/Examples_of_language_families.gif" /></div>
               <div class="img-container"><img data-enlargeable alt="Flags of europe" src="imgs/all-flags-of-europe-vector-illustration-flag-set-PXJEK1.jpg" /></div>
               <div class="img-container"><img data-enlargeable alt="Spanish Vocab" src="imgs/spanish-vocab.jpg" /></div>
       </div>
    </div>

    <script type="text/javascript">
        $('img[data-enlargeable]').addClass('img-enlargeable').click(function () {
            var src = $(this).attr('src');
            var modal;
            function removeModal() { modal.remove(); $('body').off('keyup.modal-close'); }
            modal = $('<div>').css({
                background: 'RGBA(0,0,0,.5) url(' + src + ') no-repeat center',
                backgroundSize: 'contain',
                width: '100%', height: '100%',
                position: 'fixed',
                zIndex: '10000',
                top: '0', left: '0',
                cursor: 'zoom-out'
            }).click(function () {
                removeModal();
            }).appendTo('body');
            $('body').on('keyup.modal-close', function (e) {
                if (e.key === 'Escape') { removeModal(); }
            });
        });
    </script>
</body>
</html>
