<?php

require_once 'config.php';
//Cogiendo categoria padre que envia la petición
$catPadre = $_POST["categoria"];

// Create connection
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Obtengo las subcategorias
$sql = "SELECT id_cat FROM categoria WHERE nombre_cat = '" . utf8_decode($catPadre) . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo $row["id_cat"];
    }
} else {
    echo "0";
}

$conn->close();

?>