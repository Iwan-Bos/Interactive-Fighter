// analog inputs
const int LDR = A0;
const int thermo = A1;

//digital inputs
const int leftPad = 0;
const int rightPad = 1;
const int reedS1 = 2;
const int reedS2 = 3;


void setup() {
  // put your setup code here, to run once:
  pinMode(leftPad, INPUT_PULLUP);
  pinMode(rightPad, INPUT_PULLUP);
  pinMode(reedS1, INPUT_PULLUP);
  pinMode(reedS2, INPUT_PULLUP);
}

void loop() {
  // put your main code here, to run repeatedly:
  char input = Serial.read();

  if (input == 'a')
  {
    int LDRvalue = analogRead(LDR);
    int thermoValue = analogRead(thermo);
    int leftValue = !digitalRead(leftPad);
    int rightValue = !digitalRead(rightPad);
    int reed1Value = !digitalRead(reedS1);
    int reed2Value = !digitalRead(reedS2);
    
    String toSend = "";
    toSend += LDRvalue;
    toSend += ",";
    toSend += thermoValue;
    toSend += ",";
    toSend += leftValue;
    toSend += rightValue;
    toSend += ",";
    toSend += reed1Value;
    toSend += reed2Value;

    Serial.println(toSend);
//    delay(10);
  }
}
