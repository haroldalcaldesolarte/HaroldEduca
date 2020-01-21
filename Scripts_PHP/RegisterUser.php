<?php

// Create connection
require_once 'config.php';
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Variables que envia el juego
$RegisterUser = $_POST["RegisterUser"];
$RegisterPass = $_POST["RegisterPass"];


$sql = "SELECT username FROM usuarios WHERE username = '" . $RegisterUser ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
	echo "Username ya existe. Intentelo de nuevo";

} else {
    $sql_insert= "INSERT INTO usuarios (username, password) VALUES ('" . $RegisterUser ."','" . $RegisterPass ."')";
    if ($conn->query($sql_insert) === TRUE ) {
    	echo "1";
    }else{
    	echo "0";
    }
}
$conn->close();

?>