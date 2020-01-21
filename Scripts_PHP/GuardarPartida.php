<?php

// Create connection
require_once 'config.php';
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Variables que envia el juego
$username = $_POST["username"];
$puntos = $_POST["puntos"];
$id_categoria = $_POST["id_categoria"];

$sql_insert= "INSERT INTO partida (username, puntos, id_categoria) VALUES ('" . $username ."'," . $puntos .",". $id_categoria .")";
if ($conn->query($sql_insert) === TRUE ) {
	echo "1";
}else{
	echo "0";
}
    
$conn->close();

?>