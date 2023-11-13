using System;
using Vector3 = UnityEngine.Vector3;

    namespace Interface
    {
        public interface IPlatformController
        {
            void AdjustPlatformSizeAndPosition();
            void CreateFinish();
            event Action<Vector3> OnTargetPositionChanged;
        }
    }