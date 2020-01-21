<?php

// Create connection
require_once 'config.php';
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username, puntos, id_categoria FROM partida ORDER BY puntos DESC";
$result = $conn->query($sql);

//create an array
$result_array = array();

if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        array_push($result_array, Array('username' => utf8_encode($row['username']), 'puntos' => $row['puntos'], 'id_categoria' => $row['id_categoria']));
    }
    echo json_encode($result_array);
} else {
    echo "0";
}
$conn->close();

?>