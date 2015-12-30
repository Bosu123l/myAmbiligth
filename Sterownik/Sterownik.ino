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


Adafruit_NeoPixel strip = Adafruit_NeoPixel(NUM_LED, PIN, NEO_GRB + NEO_KHZ800);

uint8_t led_color[NUM_DATA];
short index = 0;

void setup()
{
	strip.begin();
	strip.setBrightness(50);
	strip.show();
	Serial.begin(115200);
}

int generateCheckSum(byte arrayOfBytes[])
{
	int sum = 0;
	int checkSum = 0;
	int Length = NUM_DATA;
	bool flaga = true;
	for (int i = Length - 2; i >= 0; i--)
	{
		int temp = arrayOfBytes[i];
		if (flaga)
		{
			temp *= 2;
			if (temp > 9)
			{
				temp -= 9;
			}
		}
		sum += temp;
		flaga = !flaga;
	}
	int modulo = sum % 10;
	if (modulo > 0)
	{
		modulo = 10 - modulo;
	}
	checkSum = modulo;
	return checkSum;
}


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
			int checkSum = generateCheckSum(led_color);
			String stringOne = String(checkSum);
			Serial.write(checkSum);

		}
	}
}