#include <Servo.h>
#define dirPin 8
#define stepPin 9


int servoCount = 6;
int servoPins[] = {2, 3, 4, 5, 6, 7};
Servo servos[6];

void setup() {
  Serial.begin(9600);
  pinMode(dirPin,OUTPUT);//Dir
  pinMode(stepPin,OUTPUT);//Step
  AttachServos();
  //servo list   servo  pin
  //claw pinch   ->0    ->d2
  //claw rotate  ->1    ->d3
  //claw joint   ->2    ->d4
  //mid joint    ->3    ->d5
  //base jointL  ->4    ->d6
  //base jointR  ->5    ->d7
}

void loop() {
  
}

void serialEvent() {
  int channel;
  int pos;

  channel = Serial.readStringUntil(':').toInt();
  pos = Serial.readStringUntil('*').toInt();

  if(0<=channel<6){
    servos[channel].write(pos);
  }
  else if(channel == 7){
    //stepper motor rotates left 
    for(int i=0; i < pos; i++){// put your main code here, to run repeatedly:
      digitalWrite(dirPin,HIGH);  //Dir
      digitalWrite(stepPin,HIGH); //Step
      delay(25);
      digitalWrite(stepPin,LOW);  //Step
      delay(25);
    }
  }
  else if(channel == 8){
    //stepper motor rotates right
    
    for(int i=0; i < pos; i++){// put your main code here, to run repeatedly:
      digitalWrite(dirPin,LOW);  //Dir
      digitalWrite(stepPin,HIGH); //Step
      delay(25);
      digitalWrite(stepPin,LOW);  //Step
      delay(25);
    } 
  }
  else if (channel == 9){
    //used on button release to end events for base rotation
  }
  else{
    
  }
  
  
}

void AttachServos() {
  for(int i = 0; i < servoCount; i++) {
    servos[i].attach(servoPins[i]);
    
  }
  
}