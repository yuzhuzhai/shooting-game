using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkBounds : MonoBehaviour {

    public float min_X, max_X;
	
	void Update () {
        MovementBounds();
	}

    void MovementBounds() {

        Vector3 temp = transform.position;

        // we cannot go below the minimum x position
        if(temp.x < min_X) {
            temp.x = min_X;
        }

        // we cannot go above the maximum x position
        if(temp.x > max_X) {
            temp.x = max_X;
        }

        transform.position = temp;

    }

} // class































