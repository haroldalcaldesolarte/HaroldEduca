<?php

require_once 'config.php';
//Cogiendo categoria padre que envia la petición
$id_pregunta = $_POST["id_pregunta"];

// Create connection
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT id_respuesta, descripcion, correcta, id_preg FROM respuesta WHERE id_preg = '" . $id_pregunta . "'ORDER BY RAND() LIMIT 4";

$result = $conn->query($sql);
$result_array = array();

if ($result->num_rows >= 4 ) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
        array_push($result_array, Array('id_respuesta' => $row['id_respuesta'], 'descripcion' => utf8_encode($row['descripcion']), 'correcta' => $row['correcta'], 'id_preg' => $row['id_preg']));
    }
    echo json_encode($result_array);
} else {
    echo "0";
}

$conn->close();

?>