<h1> iKiwi </h1>

iKiwi is a simple program for file sharing, absolutely free and open source. 
It uses a new protocol for file sharing: Nova protocol. 
iKiwi uses a net of peers totally decentralized.

licensed under GPLv3. Please read the license file.



<h1> Nova Protocol </h1>

<img src="https://lh4.googleusercontent.com/MdYHDnEwXifm_WfzcDs-cqp47l95Isuv9MpkajswYkQ=w166-h207-p-no">

<h3>Version 0.4</h3> 

<h3>General</h3>

This is a pure P2P protocol. 


Every peer (node) must have a unique personal ID and a XML-List of other peers connected with him. 

The text of the messages is encoded in UTF16. 

The IP-address is in IPv4 protocol. 

All the date and time informations are in UTC format. 


<h3>XML-List</h3>

The xml-list containe a list of informations of the other peers actualy connected; this informatios are: ID, IP, a list of shared files, the time of start of connection. 

The structure:

<pre>
 [ ID of peer ]
 
 - [ IP of peer ]
 	
 - [ data of start-connection, UTC dd/mm/yyyy hh:mm:ss]
 	
 - [ files ]
 
 --   [ hash-ID SHA1 of file ]
 
 --   [ file-name ]
 
 --   [ size of file in bytes ]
</pre>

A example:

    <?xml version="1.0"?>
	     <Peers>
	       <_A2CCDE234EEA>
		        <IP>79.45.174.39:503</IP>
		        <Time>01/01/0001 00:00:00</Time>
		        <Files>
		           <_358C1AFEE814C1C7AA4073B10A41394FD69618CF>
			          <Name>clint.png</Name>
			          <Size>382775</Size>
		           </_358C1AFEE814C1C7AA4073B10A41394FD69618CF>
		        </Files>
	       </_A2CCDE234EEA>
	     </Peers>
<br>
<b>WARNING:</b> The char '_' must be before in the [ID of peer] and in the [hash-ID SHA1 of file].

<br>

<h3>The ID of peer</h3>

The ID of peer is unique and personal. When a person open for the first time the his p2p-client, it create a its ID that will not never change. 

The procedure for calculate a Peer-ID is this: 

Create a string with the date and time ( timezone 0, with milliseconds, from 00:00 to 24:00) of creation, add to it the IPv4 with the open port, add a series of casual numbers ( recommend a minimum of 10 casual numbers); on end generate from the string a hash-code in SHA1. 

A example of the structure of string: dd/mm/yyyy-hh:mm:ss:nnnnnn-255.255.255.255:165-xxxxxxxxxx ( x = casual number ) 

Example: 

    16/05/2010-15:46:21:534676-79.11.22.33:165-5786775460  // encode to SHA1
<br>

<h3>Start a tcp connection</h3>

This the procedure to establish a tcp connection from peer to peer; 


A sends to B: 

<pre>
Nova 0.1

CONNECT

Local-IP: 1.2.3.4:165

Remote-IP: 5.6.7.8:165

User-Agent: iKiwi 1.0
</pre> 


If B accepts the connection of A: 

<pre>
Nova 0.1

CONNECT_OK

Local-IP: 5.6.7.8:165

Remote-IP: 1.2.3.4:165

User-Agent: iKiwi 0.9.5
</pre>


Else:
 
<pre>
Nova 0.1

CONNECT_NO

Local-IP: 5.6.7.8:165

Remote-IP: 1.2.3.4:165

User-Agent: iKiwi 0.9.5
</pre> 


A simple description of these messages: 

<pre>
Nova [ version ] it say what version of protocol is used (Nova 0.1 or Nova 3.x or Nova 9.87 ... ).

CONNECT_[ OK / NO ] if it is CONNECT_OK the connection is established, else if it is CONNECTION_NO the connection is not possible.

Local-IP: [ IP:Port ] the IP:Port of peer that send the request-message.

Remote-IP: [ IP:Port ] the IP:Port of peer that receive the request-message.

User-Agent: [ User-agent-name ] the p2p-client in use.
</pre> 


After that the connection was established, A and B sends to between them their informations with the Ping-Pong messages: A send to B a PI-message, B reply to A with PO-message. 


This the procedure: 
<pre>
A to B: CONNECTION

B to A: CONNECTION_OK

A to B: PI-message

B to A: PO-message
</pre>

<br>

<h3>Message encryption</h3>

<b>Info:</b> For a major privacy is recomended to encrypt the messages.


Every peer can decide if encrypts or not his sent messages and if accept or not only the encrypted messages.

A encrypted message has only the parameters encrypted (see the "Structure of a message" paragraph).

The parameters of the messages are encrypted with the AES_128 symmetric encryption algorithm and the keys are sent using the DSA asymmetric encryption algorithm.


When two peers establish a connection they send each other a EI ( [[#EI_(_Encryption Info_) | Encryption Info]] ) message where is indicated if the peer encrypts his messages or not and if he use the message encryption indicates the public asymmetric encryption key for establish a secure stream.

Then the peer that has accepted the connection request with the public asymmetric encryption key of the other peer ( communicated with the EI message ) communicates the symmetric encryption key and establishes a secure stream.

<b>Warning:</b> Is recomended for a major security use a different symmetric encryption key for each peer.


For establish a secure stream between two peers it is need that both the peers use the message encryption.

If one of the two peers does not use the message encryption the secure stream can not be established.

If the peer that has sent the connection request does not support the message encryption he must send the EI-message anyway; the peer that has accepted the connection can decides to keep open the connection or close it.

If the peer that has sent the connection request supports the message encryption but the peer that has accepted the connection does not use it, he ( the peer that has accepted the connection ) must send a EI-message where indicates that he does not use the message encryption; the other peer can decides to keep open the connection or close it.

<h4>Example of messages</h4>

This is a not-encrypted message:

<pre>
FF 

11bb33dd55

0

60

FN pippo.txt

SZ 70

ID 123abc456
</pre>

And this is a encrypted message:

<b>Warning:</b> When a message is encrypted the length of the parameters may change, for this reason is important insert into the message the length of the ENCRYPTED parameters.

<pre>
FF 

11bb33dd55

0

65 // the new length of the encrypted parameters

a53fd26scv

5fdstt

fytfytty467
</pre>

<h4>The procedure</h4>

This is the procedure:

<pre>
A to B: CONNECTION

B to A: CONNECTION_OK

A to B: EI-message ( A uses the message encryption )

B to A: EK-message ( also B uses the message encryption )

A <-> B: secure stream
</pre>


For establish a secure stream between two peers it is need that both the peers use the message encryption.

If one of the two peers does not use the message encryption the secure stream can not be established.


For example:

<pre>
A to B: CONNECTION

B to A: CONNECTION_OK

A to B: EI-message ( A does not use the message encryption )

B: can decides if keep open the connection or not
</pre>

or

<pre>
A to B: CONNECTION

B to A: CONNECTION_OK

A to B: EI-message ( A uses the message encryption )

B to A: EI-message ( B does not use the message encryption )

A: can decides if keep open the connection or not
</pre>

<br>

<h3>Messages</h3>

The messages from peer to peer are encoded in UTF16. 

<h4>Structure of messages</h4>

This the general structure for the messages: 

<pre>
[ Command_name ] \n

[ Message_ID ] \n

[ TTL ] \n

[ Parameters_length ] \n

[ Parameters ]
</pre> 


This is the description of the structure: 

<h5>[ Command_name ]</h5>

it is the name of command of message, the its scope; 

<h5>[ Message_ID ]</h5>

the ID of message, it is used from peers for control that a message have not already procressed in the past. The peers must have a list of ID of the last message received ( recommend the last 50-100 messages ). 

for create the message-ID must do a string as this: peer-ID(SHA1) + data + time(with milliseconds) + 10(recommended) casual numbers and encode it to SHA1 
<pre>[peer-ID]-dd/mm/yyyy-hh:mm:ss:nnnnnn-xxxxxxxxxx  // SHA1 encode.</pre> 

<b>Warning:</b> When a peer spread a message he MUST NOT change or modify the ID of message.

<b>Info:</b> The ID of message, it is used from peers for control that a message have not already procressed in the past.

<h5>[ TTL ]</h5>

the time to live of the message, when a peer receive a message he must subtract 1 from TTL ( TTL-- ) first to spread it to other peers. when a message has a TTL = 0 it was not spread to other peers. 

example: 
<pre>
A send a message with TTL=2 to B,

A 2---&gt; B 1---&gt; C 0---&gt; D ( D is the last peer that receive the message ).
</pre>

<h5>[ Parameters_lenght ]</h5>

it is the length in bytes of the list of parameters. It is used by peer that receive the message for know where is the end of message. 

<h5>[ Parameters ]</h5>

it is a list of indispensable elements for the scope of message. 


<h4>The Commands</h4>

This protocol support a various type of commands and this is the list of them: 

	- FS ( File Search )
	- FF ( File Found ) 
	- FPR ( File Pack Request ) 
	- FP ( File Pack ) 
	- PI ( Ping ) 
	- PO ( Pong ) 
	- XLR ( XML-List Request ) 
	- XL ( XML-List )
	- EI ( Encryption Info )
	- EK ( Encryption Key )

<h2>How to use the messages</h2>

Here will was illustrated as to use the messages and their commands 

<h3>FS ( File Search ) &amp; FF ( File Found )</h3>

When a peer wants search a shared file he must send to other peers the messages with the FS command ( File Search ). 
The peer that has received the FS-message must control if he has the file searched in his shared directory/ies and control if other peers in his Xml-List have the file; in this time the peer that has received the FS-message must spread it to other peers if the TTL &gt; 0. 

If the peer has the file he will send a message with the FF command ( File Found ) to the peer that searched the file. 

If the peer find a other peer/s in his Xml-List that has the file he will send the FS-message to this peer. 

Every peer that received a FS-message ( or start to send it ) must spread it to other 10 ( if is possible ) peers ( first the peers that have the file in Xml-List ) if the TTL != 0. 

The recommended TTL for a FS-message is 5-6, ( 10^6 = 1 million of peers! ).

Every peers is free to resize the TTL if it is much big, a example: <br>
if A send to B a message with TTL=15 A 15---&gt; B 5---&gt; C ... B resizes TTL to 5. 

Is logic that the TTL of a FF-message is 0. 

<b>Important:</b> The file name must be at least 4 characters long, except "." and "*" from them; if a file name doesn't respect this observance the message must be deleted and mustn't be spreaded. 

Example: "pippo", "pippo*" and "pi**o1" are ok, but no "pi**o". 

<br> 

The structure of parameters: 

<h4>FS-message</h4>
<pre>
[ IP:Port-searcher ] \n

FN [ file-name ]
</pre>
 
<br>

This is the description: 


<h5>[ IP:Port-searcher ]</h5>

the IP:Port of peer that start to search the file, the first ( called Searcher ). 

It is used by the peers that found the searched file for establish a tcp-connection with the Searcher and send to he a FF-message. 


<h5>FN [ file-name ]<h/5>

the name of searched file ( example: FN photo.jpg or FN pippo ).

<br>

=== FF-message  ===

<pre>
SF [ searched-file-name ] \n

FN [ file-name ] \n

SZ [ file-size ] \n

ID [ file-SHA1-ID ]
</pre> 


This is the description: 


''' SF [ searched-file-name ]  '''

the name of searched-file. It is used by the searcher for know what file he has searched. 

<br> 

''' FN [ file-name ]  '''

the name of file found. 

<br> 

''' SZ [ file-size ]  '''

the size of file found in bytes. 

<br> 

''' ID [ file-SHA1-ID ]  '''

the SHA1-hash-ID of file found. 

<br> 

the parameters SZ and ID of a file must be after the parameter FN of that file: 

<pre>
// this a list of files found

// the informations of the first file

FN ...\n // 1

SZ ...\n // 1

ID ...\n // 1

// the informations of the second file

FN ...\n // 2

SZ ...\n // 2

ID ...   // 2
</pre>

<br>

=== Example  ===

A wants find a file called pippo and send a FS-message to B: 

<pre>
FS

aa22cc44ee

5

25 // bytes, it is a casual number

11.22.33.44:165

FN pippo
</pre> 

B has more files called pippo ( pippo.txt, superpippo.exe, Pippo2.png ) and he send a FF-message to A with the list of file found: 

<pre>
FF 

11bb33dd55

0

48

FN pippo.txt

SZ 70

ID 123abc456

FN superpippo.exe

SZ 23867

ID 789zxc654

FN Pippo2.png

SZ 8192

ID fff444aaa
</pre>
 
In this time B spread the FS- message to other peers, B send the FS-message ( of A ) to C: 

<pre>
FS 

aa22cc44ee

4 // B has done TTL--

25

11.22.33.44:165

FN pippo
</pre> 

And C control if has the file and spread the message... When TTL=0 the FS-message will not was spreaded to other peers.

<br>

== FPR ( File Pack Request ) &amp; FP ( File Pack )  ==

In this protocol the shared files are divided in more small packs, called File-Pack, with a fixed size of 16 kilobytes ( 16384 bytes ). For this when a peer want download a file he must download one to one all the File-Packs of it. 

For download a File-Pack must use the FPR command ( File Pack Request ) and for send a File-Pack must use the FP command ( File Pack ). 

The TTL must be 0 for the FPR and FP messages, and also if the TTL is not 0, for error, this messages MUST NOT spread. 

The number-position of the first byte of a file is always 0.
 

{{Info|text=The size of File-Packs is always 16 kilobytes ( 16384 bytes ).}}

<br> 

The structure of parameters: 

=== FPR-message  ===
<pre>
FN [ file-name ] \n

ID [ file-SHA1-ID ] \n

ST [ start-point ]
</pre> 


This is the description: 


''' FN [ file-name ]  '''

the name of the file, it is used by peer that receive the FPR-message for know what file must open for create and send the file-pack. 

<br> 

''' ID [ file-SHA1-ID ]  '''

the SHA1-ID ( hash ID ) of file, it is used by peer that receive the FPR-message for know what file must open for create and send the file-pack. 

<br> 

''' ST [ start-point ]  '''

it is the point to start the file-pack, in bytes. It is used for know where start the file-pack in file. 

<br>

=== <br> FP-message  ===
<pre>FN [ file-name ] \n

ID [ file-SHA1-ID ] \n

PI [ pack-SHA1-ID ] \n

ST [ start-point ] \n

BN \n

[ binary-pack ]</pre> 
<br> This is the description: 

<br>

''' FN [ file-name ] '''

the name of the file, it is used by peer that receive the FP-message for know who is the file-proprietary of the file-pack. 

<br> 

''' ID [ file-SHA1-ID ]  '''

the SHA1-ID ( hash ID ) of file, it is used by peer that receive the FP-message for know who is the file-proprietary of the file-pack. 

<br> 

''' PI [ pack-SHA1-ID ]  '''

the SHA1-ID ( hash-ID ) of the file-pack, it is used for control that the binary-file-pack received is not damage: 

when the peer receive a FP-message he must generate a SHA1-ID of binary-file-pack received and compare it with the pack-SHA1-ID of the message, if they are == the file-pack is correct, else (&nbsp;!= ) the file-pack received is damaged and the peer must re-send the FPR-message. 

<br> 

''' ST [ start-point ]  '''

it is the point to start the file-pack, in bytes. It is used for know where start the file-pack in file. 

<br> 

''' BN \n  '''

''' [ binary-pack ]  '''

it is the binary of the file-pack. It is used for create the file. This part must be remain in binary-form during all the transit-time ( do not encode or decode to or from UTF16, if necessary it is possible use the Base64 format for manage the message when arrived ). 

<br>

=== Example  ===

A want download a shared file of B; 

A send to B a FPR-message: 
<pre>FPR 

112233

0

63 // casual

FN pippo.png

ID 11bb33dd

ST 0
</pre> 

B send to A the file-pack with a FP-message:
 
<pre>
FP

556677

0

16888

FN pippo.png

ID 11bb33dd

PI aaa999

ST 0

BN

0110001110000101010111000101...........................
</pre> 

A control that the file-pack is not damaged, create the file and send the next FPR-message: 

<pre>FPR

778899

0

36

FN pippo.png

ID 11bb33dd

ST 16384</pre>

== <br> PI ( Ping ) &amp; PO ( Pong )  ==

In time the XML-List become old and is important update it. For this scope is born the PI and PO commands. 

The PI and PO-message contain the informations of the peer that send the message. 

A send to B a PI-message with the A's informations and B respond with a PO-message with the B's informations, now A and B have updated one peer-item of ther XML-List. 

The Ping-Pong messages is already used after creating a new TCP-connection. 

<br> 

The structure of parameters: 

=== PI-message &amp; PO-message  ===
<pre>PID [ peer-ID ] \n

FN [ file-name ] \n

SZ [ file-size ] \n

ID [ file-SHA1-ID ]</pre> 

If the peer hasn't shared files the&nbsp;structure of parameters will be this: 

<pre>PID [ peer-ID ]
</pre> 

<br> This is the description: 

<br> 

''' PID [ peer-ID ]  '''

the ID of the peer that send the message. 

<br> 

''' FN [ file-name ]  '''

the name of a shared file. 

<br> 

''' SZ [ file-size ]  '''

the size of the shared file. 

<br> 

''' ID [ file-SHA1-ID ]  '''

the SHA1-hash-ID of the shared file. 

<br> 

The parameters SZ and ID of a file must be after the parameter FN of that file. 

The TTL=0 in these messages. 

In a PI-PO-message there is a list of all shared files of the peer that send the message: 

<pre>
// this a list of files found 

// the informations of the first file 
FN ... \n // 1

SZ ... \n // 1

ID ... \n // 1

// the informations of the second file

FN ... \n // 2

SZ ... \n // 2

ID ...    // 2</pre>

=== Example  ===

A send to B a PI-message with his informations ( A has 3 shared files ): 
<pre>PI

11bb33dd55

0

48

PID 123456

FN pippo.txt

SZ 70

ID 123abc456

FN superpippo.exe

SZ 23867

ID 789zxc654

FN Pippo2.png

SZ 8192

ID fff444aaa</pre> 
<br> And B replies to A with a PO-message: 
<pre>PO

aa22cc44ee

0

30

PID 789012

FN photo.jpg

SZ 8192

ID aaabbbccc444</pre> 
<br>

== XLR ( XML-List Request ) & XL ( XML-List )  ==

When a peer want to get a XML-List ( example: he has just entered in the net ) he will send a XLR-message and the peer that received it will reply with a XL-message that contains the XML-List in binary form. For these messages the TTL = 0 and they mustn't be spreaded to other peer. 

<br> 

The structure of parameters: 

=== XLR-message  ===

This message hasn't parameters. 

<br> 

=== XL_message<br>  ===
<pre>
BN \n

[ binary-XML_List ]
</pre> 


This is the description:


''' BN \n  '''
''' [ binary-XML_List ]  '''

it is the binary of the XML-List. It is used from the peer that receive the message for build a XML-List. 

<br>

=== Example  ===

A send to B a XLR-message: 
<pre>
XLR

11bb33dd55

0

0
</pre> 
B reply to A with a XL-message that&nbsp;contains his xml-list updated ( if possible ): 
<pre>
XL

aa22cc44ee

0

234

BN

010001111101101101110111011........ // binary
</pre>

<br>

== EI ( Encryption Info ) & EK ( Encryption Key ) ==

The EI and EK messages are used when two peers wants establish a secure stream between them.


=== EI-message  ===

The EI command is used to communicate the public asymmetric encryption key to another peer for establish a secure stream.

The EI command is also used to communicate that the sender peer doesn't uses the encryption.

The TTL must be 0 for the EI messages, and also if the TTL is not 0, for error, this messages MUST NOT spread.

{{Warning|text=The EI message is NEVER encrypted.}}


The structure of parameters:

<pre>
EU [ y / n ] \n

AEK [ public asymmetric encryption key ]
</pre>


This is the description:


''' EU [ y / n ]  '''

it indicates if the sender peer uses the message encryption ( y ) or not ( n ).


''' AEK [ public asymmetric encryption key ] '''

the public asymmetric encryption key.

If the sender peer doesn't use the message encryption this parameter is not used.


=== EK-message ===

It is used to communicate the symmetric encryption key to a peer for establish a secure stream.

{{Warning|text=The EK message is ALWAYS encrypted with the public asymmetric encryption key of the EI message.}}


The structure of parameters:

<pre>
EK [ symmetric encryption key ]
</pre>


This is the description:


''' EK [ symmetric encryption key ]  '''

the symmetric encryption key.


=== Example  ===

A communicates to B that uses the message encryption and his public asymmetric encryption key:

<pre>
EI

aa22cc44ee

0

50 // bytes, it is a casual number

EU y

AEK abcdefg12345
</pre>

and B communicates to A that doesn't use the message encryption:

<pre>
EI

bb33dd55ff

0

20 // bytes, it is a casual number

EU n
</pre>

if B had used the message encryption A would have sent to him a EK-message:

<pre>
EK

998877gghh

0

20 // bytes, it is a casual number

EK hiu34984fubbu // it is encrypted
</pre>

<br>
