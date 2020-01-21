<?php

// Create connection
require_once 'config.php';
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Variables que envia el juego
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$sql = "SELECT password FROM usuarios WHERE username = '" . $loginUser ."'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    while($row = $result->fetch_assoc()) {
        if ($row["password"] == $loginPass) {
        	echo "Login success";
        }else{
        	echo "Password incorrecto";
        }
    }
} else {
    echo "Usuario no existe";
}
$conn->close();

?>