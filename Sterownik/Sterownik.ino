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
String odebrane;
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

	int licznik = 0;
	int tablica[3];
	int dioda = 0;
	if (Serial.available() > 0)
	{
		odebrane = Serial.readString();

		int przecinek = 0;
		for (int i = 0; i < odebrane.length(); i++)
		{
			String buffor;
			if (odebrane[i] == ',')
			{
				przecinek++;
				if (przecinek == 3)
				{
					if (dioda > 24)
					{
						dioda = 0;
					}
					przecinek = 0;
					strip.setPixelColor(dioda, tablica[0], tablica[1], tablica[2]);
					dioda++;
				}
				else
				{
					buffor += odebrane[i];
				}
				tablica[przecinek] = buffor.toInt();

			}
			
		}
		Serial.write("Oki\n");
		/*for (int i = 0; i < odebrane.length(); i += 8)
		{
			String Receive;
			for (int j = i; j < i + 8; j++)
			{
				Receive += odebrane[j];
			}
			if (licznik > 24)
				licznik = 0;
			long number = (long)strtol(&Receive[2], NULL, 16);
			int r = number >> 16;
			int g = number >> 8 & 0xFF;
			int b = number & 0xFF;
			strip.setPixelColor(licznik, r, g, b);
			licznik++;

		}
		strip.show();
		Serial.write("oki");*/
	}
}
