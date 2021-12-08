// Copyright Epic Games, Inc. All Rights Reserved.

#include "ProjectConversionGameMode.h"
#include "ProjectConversionCharacter.h"
#include "UObject/ConstructorHelpers.h"

AProjectConversionGameMode::AProjectConversionGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPersonCPP/Blueprints/ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
