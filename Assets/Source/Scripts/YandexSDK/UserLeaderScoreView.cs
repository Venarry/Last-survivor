using TMPro;
using UnityEngine;

namespace YSDK
{
    public class UserLeaderScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rank;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void Set(int rank, string name, int score)
        {
            _rank.text = rank.ToString();
            _name.text = name;
            _score.text = score.ToString();
        }
    }
}