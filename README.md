# PassKeep Android Version DON'T USE IT FOR REAL PURPOSES, ONLY FOR TESTING!!!!
Keeps a pass encrypted, just for experimental purposes<BR><BR>
LOL, it just works, BUILD: start Visual Studio 2015/2017, start the solution and make sure the startup project points to the android app instead of the DLL project, and press BUILD, yep, that's it, no required extra nonsense<BR><BR>
This is my practical lab test environment to store and read data encrypted (in this case a pass), based on a encrypt/decrypt key + cryptography such as ED25519. It anonimes the website you store (ED25519) and encrypts the pass for that website with cryptography (in this test Rijndael). This encrypted combination is stored under an anonym bin file and can be read again by using the decryption key and returns your pass for that website if you earlier stored it. For example a website 12345 is stored as "dC8Mcccx9CobL8RcwQhPGcByKFpNP2A3CK7xqRUmi8P4.bin".<BR><BR> I tested it on Android 4.4 and worked (for now I've set Android 4.3 as minimal version)<BR><BR>
DON'T USE IT FOR REAL PURPOSES, ONLY FOR TESTING!!!!
