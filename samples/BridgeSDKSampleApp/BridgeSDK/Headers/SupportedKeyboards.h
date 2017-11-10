#pragma once
#include <string>
#include <vector>

using namespace std;

struct Skin
{
	string name;
};

struct Keyboard
{
	string name;
	vector<Skin> skins;
};

struct SupportedKeyboards {
	vector<Keyboard> keyboards;
};