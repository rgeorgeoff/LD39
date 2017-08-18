<?php
$servername = "ld39.cf5jaja8jsdu.us-east-1.rds.amazonaws.com";
$username = "teddy";
$password = "Ottem55!!";
$dbname = "LD39";
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed DAMN IT: " . $conn->connect_error);
} 

$sql = "SELECT * FROM deaths";
$result = mysqli_query($conn, $sql);


if (mysqli_num_rows($result) > 0) {
    while($row = mysqli_fetch_assoc($result)) {
        echo $row["x"].",".$row["y"].",".$row["name"].",".$row["icon"].",".$row["message"].",".$row["team"]."<br>";
    }
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();
?>