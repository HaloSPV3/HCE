// SPDX-License-Identifier: GPL-3.0-only

#ifndef CHIMERA_INTERPOLATE_FP_HPP
#define CHIMERA_INTERPOLATE_FP_HPP

namespace Chimera {
    /**
     * Interpolate first person.
     */
    void interpolate_fp_before() noexcept;

    /**
     * Uninterpolate first person.
     */
    void interpolate_fp_after() noexcept;

    /**
     * Set the tick flag, swapping buffers for the next tick.
     */
    void interpolate_fp_on_tick() noexcept;
}

#endif
