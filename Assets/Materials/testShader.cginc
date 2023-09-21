
int hellCrackCount = 11;

float _hellCracks[hellCrackCount];
 
void ExampleFunction_float(out float Sum){
    for (int i=0;i<hellCrackCount;i++){
        Sum += _hellCracks[i];
    }
}

