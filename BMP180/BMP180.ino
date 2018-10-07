#include <SFE_BMP180.h>
#include <Wire.h>
#include <BluetoothSerial.h>

SFE_BMP180 pressure;
BluetoothSerial SerialBT;

void setup(){
    SerialBT.begin("ESP32");
    pressure.begin();
}

void loop(){
    double P;
    P = getPressure();
    SerialBT.println(P, 4);
    delay(5000);
}

double getPressure(){
    char status;
    double T,P,p0,a;

    status = pressure.startTemperature();
    if (status != 0){
        // ожидание замера температуры
        delay(status);
        status = pressure.getTemperature(T);
        if (status != 0){
            status = pressure.startPressure(3);
            if (status != 0){
                // ожидание замера давления
                delay(status);
                status = pressure.getPressure(P,T);
                if (status != 0){
                    return(P);
                }
            }
        }
    }
}
