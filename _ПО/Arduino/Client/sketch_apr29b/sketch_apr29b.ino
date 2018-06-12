/*Клиент*/
#include <SPI.h>
#include <Mirf.h>
#include <nRF24L01.h>
#include <MirfHardwareSpiDriver.h>


#define SendCommandTimeout 3000
#define ScanTimeout 100

#define StringLen 20 //Длина строк в структурах

typedef struct _REQUEST_STRUCTS { //Структура запроса

  int Status; //0 - всё ок, 1- ошибка;
  int Request;
  byte Comment[StringLen];

}SendRequestStruct;

typedef struct _ANSWER_STRUCTS { //Структура ответа

int Status; //0 - всё ок, 1- ошибка;
int Answer;
byte Comment[StringLen];

}SendAnswerStruct;

void setup(){
  Serial.begin(9600);
  Mirf.cePin = 8;
  Mirf.csnPin = 7;
  Mirf.spi = &MirfHardwareSpi;
  Mirf.init(); 
  Mirf.setRADDR((byte *)"serv");
  Mirf.payload = sizeof(unsigned long);
  Mirf.config();
  Serial.println("Beginning...");
}

void loop() {
  SendRequestStruct request;
  request.Status=0;
  request.Request=123;
  memcpy(&request.Comment, &"test", StringLen);
  Serial.print("Sending ");
  Serial.println((char*)request.Comment);
  Mirf.setTADDR((byte*)"0001");
  Mirf.send((byte *)&request);
  while(Mirf.isSending()){};
  delay(1000);
}






