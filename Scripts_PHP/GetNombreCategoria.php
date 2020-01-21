<?php

require_once 'config.php';
//Cogiendo categoria padre que envia la petición
$id_cat = $_POST["id_cat"];

// Create connection
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Obtengo las subcategorias
$sql = "SELECT nombre_cat FROM categoria WHERE id_cat = '" . $id_cat . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        echo utf8_encode($row['nombre_cat']);
    }
} else {
    echo "0";
}

$conn->close();

?>