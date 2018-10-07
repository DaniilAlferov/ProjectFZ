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
  float t = dht.readTemperature();
  float f = dht.readTemperature(true);

  if (isnan(h) || isnan(t) || isnan(f)) {
    SerialBT.println("Датчик не может считать данные!");
    return;
  }

  float hif = dht.computeHeatIndex(f, h);
  float hic = dht.computeHeatIndex(t, h, false);
  
  SerialBT.print("Humidity: ");
  SerialBT.print(h);
  SerialBT.print(" %\t");
  delay(100);
  SerialBT.print("Temperature: ");
  SerialBT.print(hic);
  SerialBT.print(" *C ");
  delay(100);
  SerialBT.print(hif);
  SerialBT.println(" *F\t");
  delay(2000);
}
