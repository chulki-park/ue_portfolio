/**
@author ckpark
@date 05/25/2017
@brief sample.
*/

#pragma once
#include "GameFramework/Actor.h"
#include "ActorBase.generated.h"

UCLASS()
class CKPARK_API AActorBase : public AActor
{
	GENERATED_BODY()
	
public:	
	
	/**
	@author ckpark
	@brief Sets default values for this actor's properties.
	@return void
	*/	
	AActorBase();

	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	
	// Called every frame
	virtual void Tick( float DeltaSeconds ) override;

private:
	float TestFloat;///< 테스트변수.
	
};
