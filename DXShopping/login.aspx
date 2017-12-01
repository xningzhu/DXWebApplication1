<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DXShopping.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="Shopping Web Site" />
    <meta name="author" content="Johnson" />
    <meta name="keyword" content="Shopping,mall" />
    <link rel="icon" href="images/favicon.ico" />

    <title>Login</title>

    <!-- Bootstrap core CSS -->
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="Content/css/signin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-signin">
        <div class="container">

        <h2 class="form-signin-heading">系統登入</h2>
        <label for="inputUser" class="sr-only">使用者帳號</label>
        <input runat="server" type="text" id="inputUser" class="form-control" placeholder="請輸入使用者帳號" required="required" autofocus="autofocus" />
        <label for="inputPassword" class="sr-only">使用者密碼</label>
        <input runat="server" type="password" id="inputPassword" class="form-control" placeholder="請輸入使用者密碼" required="required" />
        <div class="checkbox">
          <label>
            <input type="checkbox" value="remember-me" /> Remember me
          </label>
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit">登入系統</button>

    </div> <!-- /container -->
    </form>
</body>
</html>
