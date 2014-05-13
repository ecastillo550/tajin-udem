<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ReservationUI.Views.Index" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reservaciones</title>
<link rel='stylesheet' id='style'  href='css/normalize.css' type='text/css' media='all' />
<link rel='stylesheet' id='Link1'  href='css/style.css' type='text/css' media='all' />
<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,700,800' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Allan:400,700' rel='stylesheet' type='text/css'>
</head>
<body>
<span class="logo">Reservación</span>
 <div id="navbar">
		<ul>
		<li><a href="login.html">Inicia Sesión</a></li>
		</ul>
		</div>
	<div id="content" >
	<div id="reservation">
	<form class="Form search">
		<h4>Reserva tu mesa</h4>
		<input type="text" name="userId" placeholder="Nombre del restaurante o cocina">
	    <%--<input class="button" id="Button2" type="submit" value="Búscar"  onclick="button2();" >--%>
        <input id="Button2" type="submit" value="Búscar" onclick ="button2();"/>
        <script>
            function button2() {
                __doPostBack('Button2', 'postback')
            }
        </script>
	</div>
	<div class="wrap">
	<aside>
	Categoría de Restaurantes
	<ul>
		<li><a href="">Mexicano</a></li>
		<li><a href="">Italiano</a></li>
		<li><a href="">Chino</a></li>
	</ul>
	</aside>
	<article id="all">
		<section id="restaurant">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
		<section id="Section1">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
		<section id="Section2">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
		<section id="Section3">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
		<section id="Section4">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
		<section id="Section5">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
		<section id="Section6">
		<h5><a href="#">Título</a></h5>
		<a href="#">Tipo de Comida</a>
		<p>Descripcion de la comida si esta rica o no donde esta</p>
		<a class="bt_reserv" href="#">Reservar</a>
		</section>
	</article>
	</div>	
	</div>
</body>
</html>
