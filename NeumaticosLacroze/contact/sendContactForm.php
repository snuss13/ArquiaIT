<?php
if(isset($_POST['email'])) {
 
    // EDIT THE 2 LINES BELOW AS REQUIRED
    $email_to = "sebastiannuss@gmail.com";
    $email_subject = "Contacto desde Web";
 
    function died($error) {
        // your error code can go here
        echo "Ha ocurrido un error al enviar su consulta. ";
        //echo "These errors appear below.<br /><br />";
        //echo $error."<br /><br />";
        echo "Por favor vuelva a intentarlo nuevamente.<br /><br />";
        die();
    }
 
 
    // validation expected data exists
    if(!isset($_GET['name']) ||
        !isset($_GET['email']) ||
        !isset($_GET['tel']) ||
        !isset($_GET['message'])) {
        died('Ha ocurrido un error en la información submitida.');       
    }
 
     
 
    $name = $_GET['name']; // required
    $email_from = $_GET['email']; // required
    $telephone = $_GET['tel']; // not required
    $comments = $_GET['message']; // required
 
    $error_message = "";
    $email_exp = '/^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$/';
 
  if(!preg_match($email_exp,$email_from)) {
    $error_message .= 'El Email ingresado no parece ser v&aacute;lido.<br />';
  }
 
  if(strlen($error_message) > 0) {
    died($error_message);
  }
 
    $email_message = "Form details below.\n\n";
 
     
    function clean_string($string) {
      $bad = array("content-type","bcc:","to:","cc:","href");
      return str_replace($bad,"",$string);
    }
 
     
 
    $email_message .= "Nombre: ".clean_string($name)."\n";
    $email_message .= "Email: ".clean_string($email_from)."\n";
    $email_message .= "Telefono: ".clean_string($telephone)."\n";
    $email_message .= "Mensaje: ".clean_string($comments)."\n";
 
// create email headers
$headers = 'From: '.$email_from."\r\n".
'Reply-To: '.$email_from."\r\n" .
'X-Mailer: PHP/' . phpversion();
@mail($email_to, $email_subject, $email_message, $headers);  
?>
 
<!-- include your own success html here -->

Gracias por contactarnos. Nos pondremos en contacto a la brevedad.
<?php