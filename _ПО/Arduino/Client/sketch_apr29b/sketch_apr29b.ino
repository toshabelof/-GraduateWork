/*   Данный скетч делает следующее: передатчик (TX) отправляет массив
     данных, который генерируется согласно показаниям с кнопки и с
     двух потенциомтеров. Приёмник (RX) получает массив, и записывает
     данные на реле, сервомашинку и генерирует ШИМ сигнал на транзистор.
    by AlexGyver 2016
*/

#include <SPI.h>
#include <MFRC522.h> // библиотека "RFID".
#include "nRF24L01.h"     // библиотека радиомодуля
#include "RF24.h"         // ещё библиотека радиомодуля

#define SS_PIN 8
#define RST_PIN 7


MFRC522 mfrc522(SS_PIN, RST_PIN);

unsigned long uidDec, uidDecTemp;  // для храниения номера метки в десятичном формате

RF24 radio(9, 10); // "создать" модуль на пинах 9 и 10

byte address[][6] = {"1Node", "2Node", "3Node", "4Node", "5Node", "6Node"}; //возможные номера труб



void setup() {
  Serial.begin(9600); //открываем порт для связи с ПК

  radio.begin(); //активировать модуль
  radio.setAutoAck(1);         //режим подтверждения приёма, 1 вкл 0 выкл
  radio.setRetries(0, 15);    //(время между попыткой достучаться, число попыток)
  radio.enableAckPayload();    //разрешить отсылку данных в ответ на входящий сигнал
  radio.setPayloadSize(32);     //размер пакета, в байтах

  radio.openWritingPipe(address[0]);   //мы - труба 0, открываем канал для передачи данных
  radio.setChannel(0x60);  //выбираем канал (в котором нет шумов!)

  radio.setPALevel (RF24_PA_MAX); //уровень мощности передатчика. На выбор RF24_PA_MIN, RF24_PA_LOW, RF24_PA_HIGH, RF24_PA_MAX
  radio.setDataRate (RF24_250KBPS); //скорость обмена. На выбор RF24_2MBPS, RF24_1MBPS, RF24_250KBPS
  //должна быть одинакова на приёмнике и передатчике!
  //при самой низкой скорости имеем самую высокую чувствительность и дальность!!

  radio.powerUp(); //начать работу
  radio.stopListening();  //не слушаем радиоэфир, мы передатчик


  Serial.println("Waiting for card...");
  SPI.begin();  //  инициализация SPI / Init SPI bus.
  mfrc522.PCD_Init();     // инициализация MFRC522 / Init MFRC522 card.

  pinMode(4, OUTPUT);
    pinMode(5, OUTPUT);
}

void loop() {
  if (Serial.available() > 0) {
    char a = Serial.read();
    Serial.println(a);
    if (a == '1')
    {
      digitalWrite(4, HIGH);
      delay(300);
      digitalWrite(4, LOW);
      delay(300);
      digitalWrite(4, HIGH);
      delay(300);
      digitalWrite(4, LOW);
    }
  }

  if ( ! mfrc522.PICC_IsNewCardPresent())
    return;
  // чтение карты
  if ( ! mfrc522.PICC_ReadCardSerial())
    return;
  // показать результат чтения UID и тип метки
  dump_byte_array(mfrc522.uid.uidByte, mfrc522.uid.size);
  Serial.println();
  digitalWrite(5, HIGH);
  delay(1000);
  digitalWrite(5, LOW);
  delay(500);
}

void dump_byte_array(byte *buffer, byte bufferSize)
{
  for (byte i = 0; i < bufferSize; i++)
  {
    Serial.print(buffer[i] < 0x10 ? " 0" : " ");
    Serial.print(buffer[i], HEX);
  }
}


