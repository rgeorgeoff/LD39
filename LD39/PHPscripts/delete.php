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

$y = $_GET["y"];
$x = $_GET["x"];
$name = $_GET["n"];

$sql = "DELETE FROM deaths WHERE x=$x AND y=$y AND name='$name'";

if ($conn->query($sql) === TRUE) {
    echo "Deleted record!";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();
?>