<?php

// Create connection
require_once 'config.php';
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Variables que envia el juego
$loggedUser = $_POST["loggedUser"];


$sql = "SELECT username FROM usuarios WHERE username = '" . $loggedUser ."'";
$result = $conn->query($sql);


if ($result->num_rows > 0) {
	echo "1";
} else {
    echo "0";
}

$conn->close();

?>