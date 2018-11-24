#include <BluetoothSerial.h>

BluetoothSerial SerialBT;

void setup() {
  SerialBT.begin("ESP32");
}

void loop() {
  SerialBT.println(1);
  delay(1000);
  SerialBT.println(2);
  delay(1000);
  SerialBT.println(3);
  delay(1000);
}
