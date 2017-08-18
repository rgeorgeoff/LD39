<?php
$servername = "ld39.cf5jaja8jsdu.us-east-1.rds.amazonaws.com";
$username = "teddy";
$password = "Ottem55!!";
$dbname = "LD39";
echo("conencting");
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed DAMN IT: " . $conn->connect_error);
} 

//http://localhost/LD39/death.php?x=2&y=2&n=kek12&m=hey%20there%20man&i=0&t=0

$y = $_GET["y"];
$x = $_GET["x"];
$name = $_GET["n"];
$message = $_GET["m"];
$icon = $_GET["i"];
$team = $_GET["t"];

$sql = "INSERT INTO deaths (x, y, name, icon, message)
VALUES ($x, $y, '$name', $icon, '$message')";

if ($conn->query($sql) === TRUE) {
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();
?>