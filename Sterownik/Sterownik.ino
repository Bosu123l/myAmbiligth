/*
 Name:		Sterownik.ino
 Created:	10/9/2015 11:04:02 PM
 Author:	gawin
*/

// the setup function runs once when you press reset or power the board
#include "Adafruit_NeoPixel-master\Adafruit_NeoPixel.h"
#define PIN 5
#define NumLED 24

Adafruit_NeoPixel strip = Adafruit_NeoPixel(NumLED, PIN, NEO_GRB + NEO_KHZ800);

void setup() 
{
	strip.begin();
	strip.show();
}

// the loop function runs over and over again until power down or reset
void loop() {
  
}
