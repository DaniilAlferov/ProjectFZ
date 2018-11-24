#include <DHT.h>
#include <BluetoothSerial.h>

BluetoothSerial SerialBT;
DHT dht(4, DHT11);

void setup() {
  SerialBT.begin("ESP32");
  dht.begin();
}
 
void loop() {
  float h = dht.readHumidity();
  float t = dht.readTemperature()
  
  float hic = dht.computeHeatIndex(t, h, false);

  SerialBT.print(hic);
  delay(1000);
}
