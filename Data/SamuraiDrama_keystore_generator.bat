@echo off

keytool -genkeypair -v^
        -alias Ko-chan_Studio^
        -sigalg SHA256withRSA^
        -keyalg RSA^
        -keysize 2048^
        -validity 10000^
        -keystore SamuraiDrama.keystore^
        -storepass KHOmT6ua ^
        -keypass au6TmOHK^
        -dname "CN=Ko-chan Studio., OU=ko-chan studio., O=ko-chan studio., C=81"^
        

pause