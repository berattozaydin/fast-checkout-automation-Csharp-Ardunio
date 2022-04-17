#include <LiquidCrystal.h>
LiquidCrystal lcd(12,11,5,4,3,2);
char number[10];
byte posnmbr;
byte pos=8;
byte butposup=0;
byte butnmbrup=0;
byte butenter=0;
char progdrm='0';
void intnmbr(){
  for(byte i=0;i<9;i++){
    number[i]='0';
  }
pos=8;
posnmbr=48;
}
void posnmbrup(){
 if(posnmbr==57){
  posnmbr=48;
  return;
  }
  posnmbr++;
  return;
}
void posup(){
  if(pos==0){
    pos=8;
    return;
    }
pos--;
  }
long pwr(int x,int y){
  if(y==0){
    return 1;
    }
  return x*pwr(x,y-1);  
}
void setup() {
 lcd.begin(16,2);
 intnmbr();
 Serial.begin(9600);
  }
void buttoncheck(){
  butposup=digitalRead(10);
butnmbrup=digitalRead(9);
butenter=digitalRead(8); 
  }
void rtrnnmbr(){
  number[pos]=posnmbr;
  long x=0;
  for(int i=8;i>=0;i--){
    
    x+=(number[i]-48)*pwr(10,8-i);
    }
   Serial.println(x);
   intnmbr();
  }
void loop() {
  if(Serial.available()){progdrm=Serial.read();}
if(progdrm=='0'){bekleme();}
  if(progdrm=='1'){paracekme(); progdrm='0';}
    if(progdrm=='2'){barkod();}

}
void bekleme(){
  
    lcd.setCursor(0,0);
  lcd.print("Program");
  lcd.setCursor(0,1);
  lcd.print("Bekleniyor...");
  lcd.setCursor(0,0);
  }

void paracekme(){
  lcd.clear();
  lcd.setCursor(0,0);
  String para=Serial.readString();
  lcd.print("Toplam Tutar:");
  lcd.setCursor(0,1);
  lcd.print(para);
  byte butyes=0;
  byte butno=0;
  while(1){
  butyes=digitalRead(10);
  butno=digitalRead(9);
  if(butyes==1||butno==1){
    if(butyes==1)
      Serial.println('1');
    else Serial.println('0');

    lcd.clear();
    lcd.setCursor(0,0);
    return;
    }
  }
  
  }
  
  
void barkod(){
  lcd.clear();
lcd.print(number);
posnmbr=number[pos];
buttoncheck();
delay(300);
buttoncheck();
lcd.clear();
number[pos]='_';
lcd.print(number);
delay(300);
buttoncheck();
lcd.clear();
if(butenter==1){
rtrnnmbr();  
butposup=0;
butnmbrup=0;
butenter=0;
  }
if(butposup==1){
  number[pos]=posnmbr;
  posup();
  posnmbr=number[pos];
butposup=0;
butnmbrup=0;
butenter=0;
}

if(butnmbrup==1){
  
   posnmbrup();
   
   butnmbrup=0;
  butenter=0;
  }
number[pos]=posnmbr;
}