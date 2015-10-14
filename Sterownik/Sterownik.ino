/*
 Name:		Sterownik.ino
 Created:	10/9/2015 11:04:02 PM
 Author:	gawin
*/

// the setup function runs once when you press reset or power the board
#include "Adafruit_NeoPixel-master\Adafruit_NeoPixel.h"
#define PIN 5
#define NUM_LED 24
#define NUM_DATA 72

#define RED(arg) (a>>16)
#define GREEN(arg) ((a>>8) & 0xff)
#define BLUE(arg) (a & 0xff)


Adafruit_NeoPixel strip = Adafruit_NeoPixel(NUM_LED, PIN, NEO_GRB + NEO_KHZ800);
String odebrane;

int liczbaLED = 24 * 3;

uint8_t led_color[NUM_DATA];
int index = 0;

void setup()
{
	strip.begin();
	strip.setBrightness(50);
	strip.show();
	Serial.begin(115200);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	if (Serial.available() > 0)
	{
		led_color[index++] = (uint8_t)Serial.read();
		if (index >= NUM_DATA)
		{
			index = 0;
			for (int i = 0; i < NUM_LED; i++)
			{
				int led_index = i * 3;
				strip.setPixelColor(i, strip.Color(led_color[led_index], led_color[led_index + 1], led_color[led_index + 2]));

			}
			strip.show();
			Serial.write("Oki\n");
		}
	}

	

}
