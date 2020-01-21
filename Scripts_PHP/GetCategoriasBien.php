<?php

require_once 'config.php';

// Create connection
$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

//Cogiendo categoria padre que envia la petición
$catPadre = $_POST["catPadre"];

if($catPadre == "null"){
	$sql = "SELECT id_cat, nombre_cat, id_cat_padre FROM categoria WHERE id_cat_padre is null AND (id_cat IN (SELECT id_cat_padre FROM categoria) OR (SELECT count(*) FROM pregunta WHERE id_categoria = id_cat)>=4)";
	
	$result = $conn->query($sql);
	$result_array = array();

	if ($result->num_rows > 0) {
	    while($row = $result->fetch_assoc()) {
	        //array_push($result_array, Array('id_cat' => $row['id_cat'], 'nombre_cat' => utf8_encode($row['nombre_cat']), 'id_cat_padre' => $row['id_cat_padre']));
	        array_push($result_array, Array('id_cat' => $row['id_cat'], 'nombre_cat' => utf8_encode($row['nombre_cat']), 'id_cat_padre' => '0'));
	    }
	    echo json_encode($result_array);
	} else {
	    echo "0";
	}
}else{
	$sql_id_padre = "SELECT id_cat from categoria where nombre_cat = '" . utf8_decode($catPadre) . "'";
	$result_id_padre = $conn->query($sql_id_padre);

	if ($result_id_padre->num_rows > 0) {
		while($row = $result_id_padre->fetch_assoc()) {
			$id_cat_padre = $row['id_cat'];
		}
	}

	$sql = "SELECT id_cat, nombre_cat, id_cat_padre FROM categoria WHERE (id_cat IN (select id_cat_padre FROM categoria) OR (select count(*) from pregunta where id_categoria = id_cat)>=4) AND id_cat_padre = " . $id_cat_padre;
	$result = $conn->query($sql);
	$result_array = array();

	if ($result->num_rows > 0) {
	    // output data of each row
	    while($row = $result->fetch_assoc()) {
	        //echo $row["id_cat"]. "," . $row["nombre_cat"]. ";";
	        array_push($result_array, Array('id_cat' => $row['id_cat'], 'nombre_cat' => utf8_encode($row['nombre_cat']), 'id_cat_padre' => $row['id_cat_padre']));
	    }
	    echo json_encode($result_array);
	} else {
	    echo "0"; //no hay categorias disponibles
	}
}

$conn->close();

?>